## Table of Contents
1. [Planteo del Problema ](#problema)
2. [Solucion del Problema ](#solucion)
3. [Solucion Tecnica](#solucion-tecnica)
4. [Ejecucion Localmente](#ejecutar-localmente)

### Planteo del Problema
***
Este es un Challenge denominado OPERACION FUEGO DE QUASAR, basado en una situación en la cual los Rebeldes deben interceptar un mensaje triangulando la posición mediante tres satélites en un plano bidimensional donde conocemos las coordenadas de 3 satélites. También tenemos una nave emisora de un mensaje cuyas coordenadas son desconocidas posicion a determinar. 
La nave emisora emitirá un mensaje hacia los tres satélites, y la información que cada satélite recibirá será la siguiente: la distancia lineal desde la nave emisora al satélite, y el mensaje, el cual puede no encontrarse completo ya que debido a interferencia (ocasionada por asteroides) algunas palabras pueden no recibirse e incluso puede recibirse el mensaje en algun satelite con delay.

### Solucion del Problema
*  **Obtencion de la Localizacion**
Para poder obtener la ubicación de la nave de carga, QuasarFireOperation implementa un algoritmo de localización basado en la técnica de Trilateración, la cual consiste en definir las funciones de tres círculos cuyo centro corresponde a la posición de cada uno de los satélites y cuyo radio corresponde a la distacia de la emision del mensaje, esto con la finalidad de encontrar los puntos (x, y) en los cuales los tres círculos se intersectan.
*  **Obtencion del Mensaje**
Para poder calcular el contenido completo del mensaje, a partir de elementos parciales recibidos en cada satélite, se armó una lista que contiene informacion de cada satelite entre ello un array de string con el mensaje emitido.  
Antes empezar a recorrer esta lista debemos determinar la longitud del mensaje para este fin debemos encontrar el array de string de menor tamaño de esta manera obtendremos el tamaño del mensaje y los array de string de mayor tamaño son los que podrian contener delay o bien que una palabra que no pueda ser determinada.
Una vez que se determina la longitud del mensaje se comienza a iterar la lista desde la última posicion hacia adelante, analizando los valores de cada posicion, y guardando en una lista de posiciones ocupadas aquellas palabras que sean distintas de vacío. Luego cuando la lista de posiciones ocupadas tenga la misma longitud que el array de string de menor tamaño podemos decir entonces que el mensaje emitido fue desifrado. 

### Solucion Tecnica
Tecnicamente debemos diseñar endpoints API REST a través de HTTP POST el cual recibe objetos en formato JSON y tambien HTTP GET para procesar la información recibida y en los casos que sea posible poder determinar la ubicación y el contenido del mensaje enviado por la nave emisora.

Esta API presenta un servicio /topsecret/ que recibe mensajes de tipo POST con el siguiente formato JSON:
** API **

- POST → /topsecret/
https://deployapiapplication.appspot.com/topsecret/

	Tipo: POST

	Respuesta para mensaje y coordenadas correctas: 200 (OK)

	Respuesta cuando los nombres de los satelites son requeridos: 400 (Bad request)

	Respuesta para cuando la posicion o el mensaje no pueda calcularse: 404 (Not Found)

- POST → /topsecret/ (200 RESPONSE)

```javascript
{
	"satellites": [
			{
			"name": "kenobi",
			"distance": 5.0,
			"message": ["este", "", "", "mensaje", ""]
			},
			{
			"name": "skywalker",
			"distance": 5.0,
			"message": ["", "es", "", "", "secreto"]
			},
			{
			"name": "sato",
			"distance": 13.0,
			"message": ["", "", "un", "", ""]
			}
	]
}
```
Si los datos son correctos, la aplicación devolverá una respuesta similar a la que se muestra a continuación:
```javascript
RESPONSE CODE: 200
{
  "message": "este es un mensaje secreto",
  "position": {
      "x": 1.0,
      "y": 1.0
  }
}
```
En el caso de que no se pudiera calcular las coordenadas de la nave objetivo o no se pudiese reconstruir el mensaje original, la aplicación devuelve un RESPONSE CODE: 404. 
```javascript
- POST → /topsecret/  (404 RESPONSE)
{
  "satellites":[
    {
      "name":"kenobi",
      "distance":100.0,
      "message":["este", "", "", "mensaje", ""]
    },
    {
      "name":"skywalker",
      "distance":115.5,
      "message":["", "es", "", "", "secreto"]
    },
    {
      "name":"sato",
      "distance":142.7,
      "message":["este", "", "un", "", ""]
    }
]
}
```
- POST → /topsecret_split/
https://deployapiapplication.appspot.com/topsecret_split/sato

	Tipo: POST

	Respuesta para mensaje y coordenadas correcta cuando la informacion de los tres satelites estan guardadas : 200 OK

	Respuesta cuando almacena temporalmente la informacion obtenida del satelite indicado en el path. (500) Server Error
	Respuesta cuando el nombre del satelite es requerida: 400 (Bad request)

Además, existe un segundo servicio /topsecret_split/ que acepta POST y GET y recibe información de un satélite a la vez, como se muestra a continuación:

- POST → /topsecret_split/{satellite_name}
```javascript
{
  "distance": 13.0,
  "message": ["", "", "un", "", ""]
}
```

- GET → /topsecret_split/ 
https://deployapiapplication.appspot.com/topsecret_split
	Tipo: GET

	Respuesta para mensaje y coordenadas correctas cuando es posible: 200 (OK)

	Respuesta cuando la informacion de los tres satelites no estan completa. (500) Server Error

Este end-point permite obtener, de ser posible cuando esten cargados los datos de los tres satelites, la ubicación de la nave de carga y el mensaje de auxilio este endpoint realiza la misma logica del endpoint /topsecret/ para calcular la coordenada.

```javascript
RESPONSE CODE: 200
{
"position": {
"x": -100.0,
"y": 75.5
},
"message": "este es un mensaje secreto"
}
```
En caso de que no se pueda ubicar la nave carguera o el mensaje no pueda ser completado, el resultado será un código de respuesta 404 al igual que cuando se ejecuta el servicio /topsecret/  .


### Ejecucion Localmente
1. Clonar el Repositorio
```javascript
Clone the project using HTPPS
 git clone https://github.com/Josayala/QuasarFireOperation.git
```
2. Abrir Visual Studio 
3. Abrir la solucion del proyecto y correrlo localmente.

### Url Repositorio en Google Cloud
La API se encuentra disponible en estos momentos en la nube para ser utilizada desde la siguiente url: 
https://deployapiapplication.appspot.com/swagger/index.html

