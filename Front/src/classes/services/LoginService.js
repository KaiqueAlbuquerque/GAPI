import Retorno from './comum/Retorno.js';

export default class LoginService{
  constructor(rotas, url = 'http://200.218.13.45/auth/api/'){
    this._rotas = rotas;      
    this._url = url;
  }

  post(dadosLogin){
      
    return fetch(`${this._url}Login`, {
        headers: {'Content-type' : 'application/json'},
        method: 'post',
        body: JSON.stringify(dadosLogin)
    })
    .then(res => {
      if(res.status == 200)
      {
        res.json()
        .then(token => {
          let tokenUsuario = token;
          localStorage.setItem('token', tokenUsuario);
          $('.toast').hide();
          this._rotas.push({ path: '/gapi/home' });
        });
      }            
      else if(res.status == 401)
      {
        $('.toast').hide();
        M.toast({html: 'Dados inválidos.', classes: 'rounded'});
      }
      else if(res.status == 403)
      {
        $('.toast').hide();
        M.toast({html: 'Não autorizado.', classes: 'rounded'});
      }
    })
    .catch(err => {
        $('.toast').hide();
        M.toast({html: 'Ocorreu um erro, por favor, tente novamente mais tarde.', classes: 'rounded'});
    });
  }

  get(){
    
    var token = localStorage.getItem('token');

    if(token == '')
    {
      this._rotas.push({ path: '/' });
      $('.toast').hide();
      M.toast({html: 'Não autorizado. Realize login para acessar este conteúdo.', classes: 'rounded'});
    }
    else
    {
      return fetch(`${this._url}Login?acesso=3`, {
        headers: new Headers({
          'Authorization': `Bearer ${token}`, 
          'Content-type' : 'application/json'
        })
      })
      .then(res => {
        if(res.status == 200)
        {
          return res.json();
        }
        else{
          Retorno.TrataRetorno(res, this._rotas);
        }
      })
    }
  }

  esqueciSenha(email){
    return fetch(`${this._url}EsqueciSenha`, {
      headers: {'Content-type' : 'application/json'},
      method: 'post',
      body: JSON.stringify(email)
    })
    .then(res => {
      return res.json();      
    })
  }
}