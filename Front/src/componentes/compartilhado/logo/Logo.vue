<template>
    <router-link :to="{ name: logo.name }" class="brand-logo"><img :src="logo.logotipo" :alt="logo.altLogo" :id="logo.id"></router-link>
</template>

<script>
export default {
    props: ['logo']
}
</script>

<style>
#logo{
    height: 60px;
    width: 150px;
}
#logoHyper{
    height: 70px;
    width: 70px;
    margin-left: 10px;
}
</style>
