import Vue from 'vue'
import App from './App.vue'

import 'materialize-css';
import 'materialize-css/dist/css/materialize.min.css';

import Chart from 'chart.js';

import VueRouter from 'vue-router';

import Paginate from 'vuejs-paginate';

import { rotas } from './rotas';

Vue.use(VueRouter);

Vue.component('paginate', Paginate)

const rota = new VueRouter({
  routes: rotas,
  mode: 'history'
});

new Vue({
  el: '#app',
  router: rota,
  render: h => h(App)
})
