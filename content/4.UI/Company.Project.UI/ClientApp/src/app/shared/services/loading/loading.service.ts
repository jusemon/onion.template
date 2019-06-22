
import { Injectable, ComponentFactoryResolver, ApplicationRef, ComponentRef, EmbeddedViewRef } from '@angular/core';
import { LoadingComponent } from './loading.component';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  private componentRef: ComponentRef<LoadingComponent>;
  displayed = false;
  timeout: number = null;
  constructor(private factoryResolver: ComponentFactoryResolver, private applicationRef: ApplicationRef) { }

  show() {
    this.timeout = null;
    if (this.displayed) {
      return;
    }
    const rootViewContainer = this.getRootComponent();
    if (typeof rootViewContainer === 'undefined') {
      this.timeout = window.setTimeout(() => { this.show(); }, 10);
      return;
    }
    const factory = this.factoryResolver.resolveComponentFactory(LoadingComponent);
    this.componentRef = factory.create(rootViewContainer.injector);
    this.applicationRef.attachView(this.componentRef.hostView);
    const domElem = (this.componentRef.hostView as EmbeddedViewRef<any>)
      .rootNodes[0] as HTMLElement;
    document.body.appendChild(domElem);
    this.displayed = true;
  }

  hide() {
    if (this.timeout !== null) {
      window.clearTimeout(this.timeout);
      this.timeout = null;
    }
    if (!this.displayed) {
      return;
    }
    this.applicationRef.detachView(this.componentRef.hostView);
    this.componentRef.destroy();
    this.displayed = false;
  }

  private getRootComponent() {
    const rootComponents = this.applicationRef.components;
    return rootComponents[0];
  }
}
