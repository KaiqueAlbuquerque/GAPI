<template>    
    <div :class="classe">
        <div class="card">
            <div class="card-content">
                <div class="row">
                    <span v-if="header.titulo" class="card-title col s6 m8">{{ header.titulo }}</span>
                    <select v-for="selects of header.select" :key="selects" class="browser-default col s3 m2 right">
                        <option v-for="options of selects.valores" :key="options" :selected="options.selected">{{ options.valor }}</option>
                    </select>
                </div>                
                
                <slot class="row">
                    
                </slot>
            </div>
        </div>             
    </div>            
</template>

<script>
export default {
    props: ['classe', 'header']
}
</script>

<style scoped>

</style>
