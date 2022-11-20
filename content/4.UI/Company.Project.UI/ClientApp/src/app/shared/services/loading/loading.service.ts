
import { Injectable, ViewContainerRef, ApplicationRef, ComponentRef, EmbeddedViewRef } from '@angular/core';
import { AppComponent } from 'src/app/app.component';
import { LoadingComponent } from './loading.component';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  private componentRef?: ComponentRef<LoadingComponent>;
  displayed = false;
  timeout: number | null = null;
  constructor(private applicationRef: ApplicationRef) { }

  show() {
    this.timeout = null;
    if (this.displayed) {
      return;
    }
    const rootViewContainerRef = this.getRootViewContainerRef();
    if (typeof rootViewContainerRef === 'undefined') {
      this.timeout = window.setTimeout(() => { this.show(); }, 10);
      return;
    }
    this.componentRef = rootViewContainerRef.createComponent(LoadingComponent);
    const domElem = (this.componentRef?.hostView as EmbeddedViewRef<any>)
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
    const rootViewContainerRef = this.getRootViewContainerRef();
    if (typeof rootViewContainerRef === 'undefined') {
      this.timeout = window.setTimeout(() => { this.hide(); }, 10);
      return;
    }
    // rootViewContainerRef.remove(this.componentRef!.hostView.);
    this.componentRef!.destroy();
    this.displayed = false;
  }

  private getRootViewContainerRef() {
    const rootComponents = this.applicationRef.components;
    const rootViewContainer = rootComponents[0] as ComponentRef<AppComponent>;
    if (typeof rootViewContainer === 'undefined') {
      return undefined;
    }
    const { instance: {viewContainerRef} } = rootViewContainer;
    return viewContainerRef;
  }
}
