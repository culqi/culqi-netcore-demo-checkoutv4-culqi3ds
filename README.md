# DEMO - Culqi Net/NetCore + Checkout V4 + Culqi 3DS

La demo integra Culqi Net/NetCore, Checkout V4 , Culqi 3DS y es compatible con la v2.0 del Culqi API, con esta demo podrás generar tokens, cargos, clientes, cards.

## Requisitos

* Net 6.0+
* Visual Studio 2022
* culqinet.dll (se genera a partir de la librería culqi-net)
* Afiliate [aquí](https://afiliate.culqi.com/).
* Si vas a realizar pruebas obtén tus llaves desde [aquí](https://integ-panel.culqi.com/#/registro), si vas a realizar transacciones reales obtén tus llaves desde [aquí](https://panel.culqi.com/#/registro) (1).

> Recuerda que para obtener tus llaves debes ingresar a tu CulqiPanel > Desarrollo > ***API Keys***.

![alt tag](http://i.imgur.com/NhE6mS9.png)

> Recuerda que las credenciales son enviadas al correo que registraste en el proceso de afiliación.

## Instalación

Para que la demo funcione se le debe instalar el archivo **CulqiNet.dll** que se genera desde la librería [Culqi-Net](https://github.com/culqi/culqi-net).

La demo ya trae dicha dll en su ruta principal.

Si deseas actualizar a la última versión de la líbrería Culqi-Net puedes descarla desde los tags

https://github.com/culqi/culqi-net/tags

## Configuración backend

Dentro del archivo **GenericController.cs** coloca tus llaves pk y sk.

```cs
public Security securityKeys()
    {
        
        security = new Security();
        security.public_key = "Llave pública del comercio (INSERTE_SU_LLAVE_PUBLICA)";
        security.secret_key = "Llave secreta del comercio (INSERTE_SU_LLAVE_PRIVADA)";
        security.rsa_id = "Id de la llave RSA";
        security.rsa_key = "Llave pública RSA que sirve para encriptar el payload de los servicios";
    
        return security;
    }
```

## Configuración frontend

Para configurar los datos del cargo, pk del comercio, rsa_id, rsa_public_key y datos del cliente se tiene que modificar en el archivo `js/config/index.js`.

```js
export default Object.freeze({
    TOTAL_AMOUNT: monto de pago,
    CURRENCY: tipo de moneda,
    PUBLIC_KEY: llave publica del comercio (INSERTE_SU_LLAVE_PUBLICA),
    RSA_ID: Id de la llave RSA,
    RSA_PUBLIC_KEY: Llave pública RSA que sirve para encriptar el payload de los servicios del checkout,
    COUNTRY_CODE: iso code del país(Ejemplo PE)
});

export const customerInfo = {
    firstName: "Fernando",
    lastName: "Chullo",
    address: "Coop. Villa el Sol",
    phone: "945737476",
}
```

## Inicializar la demo

Ejecutar la demo desde Visual Studio 2022.

## Probar la demo

Para poder visualizar el frontend de la demo ingresar a la siguiente URL:

- Para probar cargos: `https://localhost:7165/vista/index.html`
- Para probar creación de cards: `https://localhost:7165/vista/index-card.html`

## Documentación

- [Referencia de Documentación](https://docs.culqi.com/)
- [Referencia de API](https://apidocs.culqi.com/)
