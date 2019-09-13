<template>
    <div class="row">
        <menu-hyper></menu-hyper>

        <div class="container">
            <div class="row">
                <meu-card :classe="tamanho" :header="headerCardCadastro">
                    <form>                        
                        <div class="row">
                            <div class="input-field col s12 m12 l4 xl4">
                                <i class="material-icons prefix">business</i>
                                <select id="clientes">
                                    <option value="" disabled selected>Selecione o Cliente</option>
                                </select>
                                <label>Cliente</label>
                            </div>
                            <div class="input-field col s12 m12 l4 xl4">
                                <i class="material-icons prefix">add</i>
                                <input id="qtdToken" type="number" class="validate" autocomplete="off" min="1">
                                <label id="lblQtdToken" for="qtdToken">Quantidade de Tokens</label>
                            </div>
                            <div class="input-field col s12 m12 l4 xl4">
                                <i class="material-icons prefix">date_range</i>
                                <input type="text" id="data" class="datepicker" autocomplete="off">
                                <label id="lblData" for="data">Validade dos Tokens</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field right">
                                <a v-on:click="adicionaTokens" class="waves-effect waves-light btn blue lighten-2 btn-small">Cadastrar</a>
                            </div>      
                        </div>         
                    </form>     
                </meu-card>                            
            </div>

            <div class="row">
                <meu-card :classe="tamanho" :header="headerCardCadastrados">
                    <minha-tabela :titulos="titulos" :corpos="corpos" :acoes="acoes"></minha-tabela>
                    <div class="row" id="paginacao">
                        <div class="right">
                            <paginate
                                :page-count="paginas"
                                :page-range="2"
                                :margin-pages="2"
                                :click-handler="mudaPagina"
                                :prev-text="'<'"
                                :next-text="'>'"
                                :container-class="'pagination'"
                                :page-class="'waves-effect'">
                            </paginate>
                        </div>
                    </div>
                </meu-card>
            </div>     
        </div>

        <meu-loader id="loaderTokensHyperspace"></meu-loader>
    </div>
</template>

<script>
//Componentes
import MenuHyper from '../menu/MenuHyper.vue';
import Card from '../../compartilhado/card/Card.vue';
import Tabela from '../../compartilhado/tabela/Tabela.vue';
import Loader from '../../compartilhado/loader/Loader.vue';

//Serviços
import TokenService from '../../../classes/services/hyperspace/TokenService.js';
import ClienteService from '../../../classes/services/hyperspace/ClienteService.js';

export default {
    components: {
        'menu-hyper': MenuHyper,
        'meu-card': Card,
        'minha-tabela': Tabela,
        'meu-loader': Loader
    },

    methods: {
        mudaPagina(page){
            this.preencheTabela((page - 1));
        },

        aguarde(){
            let loader = document.getElementById("loaderTokensHyperspace");
            loader.style.display = 'block';
        },

        naoAguarde(){
            let loader = document.getElementById("loaderTokensHyperspace");
            loader.style.display = 'none';
        },

        preencheTabela(pagina){
            this.aguarde();

            let tokenService = new TokenService(this.$router);
            tokenService.consultaTodos(pagina)
                .then(tokens => {
                    this.corpos = [];
                    this.arrayTokens = [];

                    this.paginas = tokens.QuantidadePaginas;
                    this.arrayClientes = tokens.Empresas;

                    tokens.Empresas.forEach(token => {
                        this.corpos.push({"id": token.Id_Empresa, "valor": [token.Nome, token.Tokens.length]});
                    });

                    if(this.paginas < 2)
                        document.getElementById("paginacao").style.display = "none";
                    else
                        document.getElementById("paginacao").style.display = "block";

                    this.naoAguarde();
                })  
                .catch(erro => this.naoAguarde())
        },

        adicionaTokens(){
            let cliente = document.getElementById("clientes");
            let tokens = document.getElementById("qtdToken");
            let validade = document.getElementById("data");

            if(this.validaCadastro(cliente, tokens, validade)){
                this.aguarde();
            
                let token = {
                                "Id_Empresa": cliente.value,
                                "Data_Expiracao": validade.value,
                                "Quantidade": tokens.value,
                            }
                let tokenService = new TokenService(this.$router);
                tokenService.cadastrar(token)
                    .then(ret => this.naoAguarde())
                    .catch(err => this.naoAguarde())

                this.reiniciaInputs();
                cliente.value = "";
                this.reiniciaSelect();
                this.preencheTabela(0);
            }
        },

        buscaClientes(){
            let clienteService = new ClienteService(this.$router);
            clienteService.consultaTodosCombo()
                .then(clientes => {
                    for(let i = 0; i < clientes.length; i++){
                        document.getElementById("clientes").options[i+1] = new Option(clientes[i].Nome, clientes[i].Id_Empresa);
                    }

                    this.reiniciaSelect();
                })
        },

        reiniciaInputs(){
            let tokens = document.getElementById("qtdToken");
            tokens.value = "";
            tokens.classList.remove("valid");
            document.getElementById("lblQtdToken").classList.remove("active");

            let data = document.getElementById("data");
            data.value = "";
            document.getElementById("lblData").classList.remove("active");
        },

        reiniciaSelect(){
            let elems = document.querySelectorAll('select');
            let options = document.querySelectorAll('option');
            let instances = M.FormSelect.init(elems, options);
        },

        validaCadastro(cliente, tokens, validade){
            var patternData = /^[0-9]{2}\/[0-9]{2}\/[0-9]{4}$/;
            
            if(cliente.value == ""){
                $('.toast').hide();
                M.toast({html: 'Informe o cliente.', classes: 'rounded'});
                return false;
            }
            else if(tokens.classList[1] != "valid"){
                $('.toast').hide();
                M.toast({html: 'Informe a quantidade de tokens.', classes: 'rounded'});
                return false;
            }
            else if(validade.value == ""){
                $('.toast').hide();
                M.toast({html: 'Informe a data de validade dos tokens.', classes: 'rounded'});
                return false;
            }
            else if(!patternData.test(validade.value)){
                $('.toast').hide();
                M.toast({html: 'Data inválida. Digite a data no formato Dia/Mês/Ano.', classes: 'rounded'});
                return false;
            }
            else{
                return true;
            }
        }
    },

    data(){
        return{
            //Controladores
            arrayTokens: [],

            //Paginação
            paginas: 0,

            //Card branco
            tamanho: "col s12",
            headerCardCadastro: {
                titulo: "Cadastro de Tokens"
            },
            headerCardCadastrados: {
                titulo: "Tokens cadastrados"
            },

            //Tabela
            titulos: ['Cliente', 'Tokens Ativos'],
            corpos: [],
            acoes: false,
        }
    },

    mounted(){
        this.$nextTick(function () {
            var elems = document.querySelectorAll('select');
            var a = M.FormSelect.init(elems);

            var elems2 = document.querySelectorAll('.datepicker');
            var a2 = M.Datepicker.init(elems2, {
                format: "dd/mm/yyyy",
                minDate: new Date(Date.now()),
                disableWeekends: true,
                yearRange: 5,
                i18n: 
                {
                    today: 'Hoje',
                    clear: 'Limpar',
                    done: 'Ok',
                    nextMonth: 'Próximo mês',
                    previousMonth: 'Mês anterior',
                    weekdaysAbbrev: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'],
                    weekdaysShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb'],
                    weekdays: ['Domingo', 'Segunda-Feira', 'Terça-Feira', 'Quarta-Feira', 'Quinta-Feira', 'Sexta-Feira', 'Sábado'],
                    monthsShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    months: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro']
                }
            });
        });

        this.preencheTabela(0); 
        this.buscaClientes();
    }   
}
</script>

<style>
.datepicker-date-display {    
    background-color: #64b5f6;    
}
.datepicker-table td.is-selected{
    background-color: #64b5f6; 
}
.pagination li.active {
    background-color: #64b5f6;
}
</style>

<style scoped>
#paginacao{
    padding-top: 15px;
    display: none;
}
.row {
    margin-bottom: 0px;
}
</style>
