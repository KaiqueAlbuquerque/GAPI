<template>
    <table class="highlight centered">
        <thead>
            <tr>
                <th v-for="titulo of titulos" :key="titulo">{{ titulo }}</th>
                <th v-if="acoes">Ações</th>
            </tr>
        </thead>

        <tbody>
            <tr v-for="corpo of corpos" :key="corpo">
                <td v-for="valor of corpo.valor" :key="valor">{{ valor }}</td>
                <td v-if="acoes">
                    <span class="link" v-on:click="alterar(corpo)">Alterar</span>
                    <span id="divisor">|</span>
                    <span class="link" v-on:click="excluir(corpo)">Excluir</span>
                </td>
            </tr>
        </tbody>
    </table>
</template>

<script>
export default {
    props: ['titulos', 'corpos', 'acoes'],

    methods:{
        alterar (obj) {
            this.$emit('alterar', obj);
        },
        excluir(obj){
            this.$emit('excluir', obj);
        }
    }
}
</script>

<style scoped>
    .link{
        color: #039be5;
        cursor: pointer;
    }
</style>
