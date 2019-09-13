export default class AutenticacaoServiceBase {
    constructor(url = 'http://200.218.13.45/auth/api/', token = localStorage.getItem('token')){       
        this._url = url;
        this._token = token;
    }
}