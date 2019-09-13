<template>
    <ul class="collection dropdown-content" id="apis">
        <li class="collection-item">
            <router-link :to="{ name:'homeGapi' }"><img src="../../../assets/gapiIcon.png" alt="gapi" id="logoGapi"></router-link>
            <router-link :to="{ name:'homeGapi' }"><span>Gapi</span></router-link>     
        </li>
        <li class="collection-item">
            <router-link :to="{ name:'homeHyperspace' }"><img src="../../../assets/hyper.png" alt="logo hyperspace" id="logoHyper"></router-link>
            <router-link :to="{ name:'homeHyperspace' }"><span>Hyperspace</span></router-link>             
        </li>  
        <li class="collection-item">
            <router-link :to="{ name:'homeMarca' }"><img src="../../../assets/MarcaIcon.png" alt="logo marca dagua" id="logoMarca"></router-link>
            <router-link :to="{ name:'homeMarca' }"><span>Marca d'agua</span></router-link>            
        </li>    
        <li class="collection-item">
            <router-link :to="{ name:'homeQrCode' }"><img src="../../../assets/QrCodeIcon.png" alt="logo qrCode" id="logoQrCode"></router-link>
            <router-link :to="{ name:'homeQrCode' }"><span>QrCode</span></router-link>            
        </li>        
        <li class="collection-item">
            <router-link :to="{ name:'homeSms' }"><img src="../../../assets/EnviaSMSIcon.png" alt="logo sms" id="logoSms"></router-link>
            <router-link :to="{ name:'homeSms' }"><span>Envia SMS</span></router-link>             
        </li>    
    </ul>        
</template>

<script>
export default {
    mounted(){
        this.$nextTick(function () {
            var elems = document.querySelectorAll('.dropdown-trigger');
            var a = M.Dropdown.init(elems , {
                coverTrigger: false,
                constrainWidth: false
            });
        });
    },
}
</script>

<style scoped>
.dropdown-content li{
    clear: none;
    width: 50%; 
    text-align: center;
}
.collection .collection-item {
    border-bottom: none;
    padding: 10px 0px;
}
.dropdown-content li>a{
    padding: 5px 0px;
}
#logoMarca{
    height: 80px;
    width: 70px;
}
#logoQrCode{
    height: 80px;
    width: 90px;
}
#logoGapi{
    height: 80px;
    width: 80px;
}
#logoHyper{
    height: 80px;
    width: 80px;
}
#logoSms{
    height: 80px;
    width: 90px;
}
.dropdown-content{
    width: 300px;
}
</style>