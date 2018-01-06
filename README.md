# AltranAssessment

Proyect BackEnd: Tips
	- Los 4 EndPoints disponibles para consumir se encuentran en: C:\Repos\Documents\PostmanFiles, sección "Altran Backend"
	- En el header Http se debe cargar el nombre de usuario para el cual se esta consumiendo en EndPoint
		* Key = user
		* Value = (usuario existente en http://www.mocky.io/v2/5808862710000087232b75ac)
	- Al ejecutar el proyecto se va a abrir en el siguiente puerto http://localhost:21809
	- Por default se muestra la documentacion de la WeaApi via Swagger: http://localhost:21809/swagger/docs/v1
	- Los errores se loguean con NLOG en: ${basedir}\Log\BackEnd.json

Proyect FrontEnd: Tips
	- Al ejecutar el proyecto se va a abrir en el siguiente puerto http://localhost:25145
	- Para ingresar a la aplicacion se debe cargar un Email valido de la siguiente lista: http://www.mocky.io/v2/5808862710000087232b75ac
	- Todos los filtros de busqueda son opcionales
	- Las grillas de busqueda se pueden ordenar por cualquiera de sus columnas
	- Todas las busquedas son paginadas
	- La aplicacion esta armada con JQuery y Typescript. Quedo pendiente la utilizacion de KnockOut o Angular
