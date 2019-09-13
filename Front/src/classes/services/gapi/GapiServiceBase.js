export default class GapiServiceBase{
    constructor(
            urlHyperspace = 'http://200.218.13.45/hyper-tokens/api/', 
            urlMarcaDagua = 'http://200.218.13.45/alterapdf/api/',
            urlQrCode = 'http://200.218.13.45/alterapdf/api/',
            token = localStorage.getItem('token')){       
        this._urlHyperspace = urlHyperspace;
        this._urlMarcaDagua = urlMarcaDagua;
        this._urlQrCode = urlQrCode;
        this._token = token;
    }
}   