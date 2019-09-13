<template>
    <div v-on:click="navega" :class="classe">
        <h4>{{ textos.titulo }}</h4>
        <span :id="idElemento">{{ textos.destaque }}</span> 
        <span><b>{{ textos.complementoDestaque }}</b></span> 
    </div>
</template>

<script>
export default {
    props: ['classe', 'textos', 'idElemento'],

    methods: {
      navega(){
        this.$emit('navegar');
      }
    }
}
</script>

<style scoped>
.infoGeralOnline{
  background: linear-gradient(to right, rgb(139,195,74), rgb(139,195,74)); 
  transform: translate3d(30px, 30px, 0px);
  height: 150px;
  text-align: center;
  color: white;
}
.infoGeralOffline{
  background: linear-gradient(to right, rgb(231,56,39), rgb(248,80,50)); 
  transform: translate3d(30px, 30px, 0px);
  height: 150px;
  text-align: center;
  color: white;
}
.infoGeralGapi{
  background: linear-gradient(to right, rgb(121, 86, 236), rgb(47, 185, 248)); 
  transform: translate3d(30px, 30px, 0px);
  height: 200px;
  border-radius:20px; 
  text-align: center;
  color: white;
}
.infoGeralMarca{
  background: linear-gradient(to right, rgb(231,56,39), rgb(248,80,50)); 
  transform: translate3d(30px, 30px, 0px);
  height: 200px;
  border-radius:20px; 
  text-align: center;
  color: white;
}
.infoGeralQrCode{
  background: linear-gradient(to right, rgb(19,19,19), rgb(76,76,76)); 
  transform: translate3d(30px, 30px, 0px);
  height: 200px;
  border-radius:20px; 
  text-align: center;
  color: white;
}
.infoGeralHyperspace{
  background: linear-gradient(to right, rgb(66,165,245), rgb(183,222,237)); 
  transform: translate3d(30px, 30px, 0px);
  height: 200px;
  border-radius:20px; 
  text-align: center;
  color: white;
}
#numeroDestaqueGapi{
  font-size: 70px;
}
#numeroDestaqueMarca{
  font-size: 50px;
}
#numeroDestaqueMonitoramento{
  font-size: 30px;
}
</style>
