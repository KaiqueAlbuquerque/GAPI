export default class HyperspaceServiceBase {
    constructor(url = 'http://200.218.13.45/hyper-tokens/api/', token = localStorage.getItem('token')){       
        this._url = url;
        this._token = token;
    }
}