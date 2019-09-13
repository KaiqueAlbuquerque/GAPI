import Login from './componentes/login/Login.vue';

//Gapi
import HomeGapi from './componentes/gapi/home/HomeGapi.vue';
import UsuariosGapi from './componentes/gapi/usuarios/UsuariosGapi.vue';
import ApiGapi from './componentes/gapi/api/ApiGapi.vue';

//Marca dagua
const HomeMarca = () => System.import('./componentes/marcaDagua/home/HomeMarca.vue').then(m => m.default);
const UsuariosMarca = () => System.import('./componentes/marcaDagua/usuarios/UsuariosMarca.vue').then(m => m.default);
const ClientesMarca = () => System.import('./componentes/marcaDagua/clientes/ClientesMarca.vue').then(m => m.default);
const TesteMarca = () => System.import('./componentes/marcaDagua/testeMarca/TesteMarca.vue').then(m => m.default);

//QrCode
const HomeQrCode = () => System.import('./componentes/qrCode/home/HomeQrCode.vue').then(m => m.default);
const UsuariosQrCode = () => System.import('./componentes/qrCode/usuarios/UsuariosQrCode.vue').then(m => m.default);
const ClientesQrCode = () => System.import('./componentes/qrCode/clientes/ClientesQrCode.vue').then(m => m.default);
const TesteQrCode = () => System.import('./componentes/qrCode/TesteQrCode/TesteQrCode.vue').then(m => m.default);

//Hyperspace
const HomeHyperspace = () => System.import('./componentes/hyperspace/home/HomeHyperspace.vue').then(m => m.default);
const ClientesHyperspace = () => System.import('./componentes/hyperspace/clientes/ClientesHyperspace.vue').then(m => m.default);
const TokensHyperspace = () => System.import('./componentes/hyperspace/tokens/TokensHyperspace.vue').then(m => m.default);

//Relatório SMS
const HomeEnviaSms = () => System.import('./componentes/enviaSMS/home/HomeEnviaSms.vue').then(m => m.default);

export const rotas = [
    //Pagina login
    { path: '', name: 'login', component: Login },

    //Gapi
    { path: '/gapi/home', name: 'homeGapi', component: HomeGapi },
    { path: '/gapi/usuarios', name: 'usuariosGapi', component: UsuariosGapi },
    { path: '/gapi/apis', name: 'apisGapi', component: ApiGapi },

    //Marca dagua
    { path: '/marca/home', name: 'homeMarca', component: HomeMarca },
    { path: '/marca/usuarios', name: 'usuariosMarca', component: UsuariosMarca },
    { path: '/marca/clientes', name: 'clientesMarca', component: ClientesMarca },
    { path: '/marca/testeMarca', name: 'testeMarca', component: TesteMarca },

    //QrCode
    { path: '/qrCode/home', name: 'homeQrCode', component: HomeQrCode },
    { path: '/qrCode/usuarios', name: 'usuariosQrCode', component: UsuariosQrCode },
    { path: '/qrCode/clientes', name: 'clientesQrCode', component: ClientesQrCode },
    { path: '/qrCode/testeQrCode', name: 'testeQrCode', component: TesteQrCode },

    //Hyperspace
    { path: '/hyperspace/home', name: 'homeHyperspace', component: HomeHyperspace },
    { path: '/hyperspace/clientes', name: 'clientesHyperspace', component: ClientesHyperspace },
    { path: '/hyperspace/tokens', name: 'tokensHyperspace', component: TokensHyperspace },

    //Envia SMS
    { path: '/sms/home', name: 'homeSms', component: HomeEnviaSms },

    //Pagina default
    { path: '*', component: HomeGapi }
];

