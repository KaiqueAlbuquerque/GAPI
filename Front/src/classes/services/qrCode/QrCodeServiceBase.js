export default class QrCodeServiceBase {
    constructor(url = 'http://200.218.13.45/alterapdf/api/', token = localStorage.getItem('token')){       
        this._url = url;
        this._token = token;
    }
}