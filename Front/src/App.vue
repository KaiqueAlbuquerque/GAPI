<template>
  <div id="tudo">
    <div id="conteudo">
      <transition name="pagina">
        <router-view></router-view>
      </transition>
    </div>
    <footer id="rodape">
      <div class="footer-copyright">
        <div class="container center" id="conteudoRodape">
          <a href="http://www.ubik.com.br" target="_blank">
            <img src="./assets/logo_ubik.jpg" alt="Logo ubik">
          </a>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
export default {
  
}
</script>

<style>
.pagina-enter, .pagina-leave-active{
  opacity: 0;
}
.pagina-leave-active{
  transition: opacity .2s;
}
.pagina-enter-active{
  transition: opacity 2s;
}
#conteudoRodape{
  border-top: 1px solid #bdbdbd;
  padding-top: 10px;
  padding-bottom: 10px;
}
#tudo{
  width: 100%;
  position: relative;
  margin: 0 auto;
  min-height: 100%;
  border-width: 0 1px;
}
#rodape {
  width: 100%;
  position: absolute;
  bottom: 0;
}
#conteudo {
  padding-bottom: 70px;
  margin: 0 auto;
}
html, body {
  height: 100%;
}
</style>
