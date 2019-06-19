# Onion Template

Una plantilla con la arquitectura Onion aplicada a .NET Core

## Empezando

### Prerequisitos

- Nuget ([enlace](https://www.nuget.org/downloads))
- .NET Core ([enlace](https://dotnet.microsoft.com/download))

### Instalando

Una vez clonado el repositorio, se deben ejecutar en el cmd las siguientes lineas en la carpeta padre:
```
nuget pack .\onion.template\Company.Project.nuspec
dotnet new -i Onion.Template.1.0.1.nupkg
```

## Creando proyectos

Una vez instalada la plantilla podemos consultar las opciones disponibles con:
```
dotnet new onion -h
```
Y para crear un proyecto nuevo usaremos:
```
dotnet new onion -o Nombre.Proyecto --title "Mi Proyecto" --company "Ejemplo SAS"
```
