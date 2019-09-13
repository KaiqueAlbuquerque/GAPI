<template>
    <div class="row">
        <menu-qrCode></menu-qrCode>

        <div class="container">
            <div class="row">
                <meu-card :classe="tamanho" :header="headerCardCadastro">
                    <form>                        
                        <div class="row">
                            <div class="input-field col s6">
                                <i class="material-icons prefix">insert_link</i>
                                <input id="link" type="text" autocomplete="off">
                                <label for="link">Link</label>
                            </div>
                            <div class="input-field col s6">
                                <i class="material-icons prefix">format_size</i>
                                <input id="tamanho" type="text" autocomplete="off">
                                <label for="tamanho">Tamanho</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s6 m6 l3 xl3">
                                <i class="material-icons prefix">swap_horiz</i>
                                <input id="xpe" type="text" autocomplete="off">
                                <label for="xpe">X Folha em pé</label>
                            </div>
                            <div class="input-field col s6 m6 l3 xl3">
                                <i class="material-icons prefix">swap_horiz</i>
                                <input id="xdeitada" type="text" autocomplete="off">
                                <label for="xdeitada">X Folha deitada</label>
                            </div>
                            <div class="input-field col s6 m6 l3 xl3">
                                <i class="material-icons prefix">swap_vert</i>
                                <input id="ype" type="text" autocomplete="off">
                                <label for="ype">Y Folha em pé</label>
                            </div>
                            <div class="input-field col s6 m6 l3 xl3">
                                <i class="material-icons prefix">swap_vert</i>
                                <input id="ydeitada" type="text" autocomplete="off">
                                <label for="ydeitada">Y Folha deitada</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="file-field input-field">
                                <div class="btn grey darken-3">
                                    <span>File</span>
                                    <input type="file" id="arquivo" accept="application/pdf" multiple>
                                </div>
                                <div class="file-path-wrapper">
                                    <input class="file-path" id="nomeArquivo" type="text" placeholder="Escolha um ou mais arquivos">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="input-field col s8">
                                <a href="http://www.asppdf.com/manual_12.html" target="_blank">Dúvidas? Clique aqui para documentação oficial</a>
                            </div>                    
                            <div class="input-field col s3 right">
                                <a v-on:click="criar" class="waves-effect waves-light btn grey darken-3 btn-small">Criar marca d'agua</a>
                            </div>
                        </div>              
                    </form>     
                </meu-card>                            
            </div>
        </div>
        <meu-loader id="loaderTesteQr"></meu-loader>
    </div>
</template>

<script>
//Componentes
import MenuQrCode from '../menu/MenuQrCode.vue';
import Card from '../../compartilhado/card/Card.vue';
import Loader from '../../compartilhado/loader/Loader.vue';

//Serviços
import Retorno from '../../../classes/services/comum/Retorno.js';

export default {
    components: {
        'menu-qrCode': MenuQrCode,
        'meu-card': Card,
        'meu-loader': Loader
    },

    methods: {
        aguarde(){
            let loader = document.getElementById("loaderTesteQr");
            loader.style.display = 'block';
        },

        criar(){
            if(this.validaCampos()){
                var fileInput = document.getElementById('arquivo');

                for(var i = 0; i < fileInput.files.length; i ++)
                {
                    this.aguarde();
                    var data = new FormData();            
                    data.append(fileInput.files[i].name, fileInput.files[i]);
                    data.append("link", document.getElementById("link").value);
                    data.append("tamanho", document.getElementById("tamanho").value);
                    data.append("xfolhaEmPe", document.getElementById('xpe').value);
                    data.append("xfolhaDeitada", document.getElementById('xdeitada').value);
                    data.append("yfolhaEmPe", document.getElementById('ype').value);
                    data.append("yfolhaDeitada", document.getElementById('ydeitada').value);
                                        
                    $.ajax({
                        type: "POST",
                        data: data,
                        url: "http://200.218.13.45/alterapdf/api/QrCode/GeraQrCodeToFront",
                        headers: {
                            "Authorization": "Bearer " + localStorage.getItem('token')
                        },
                        cache: false,
                        contentType: false,
                        processData: false,
                    })
                    .always(function (data, textStatus, xhr) {
                        if(xhr.status == 200){
                            var link = document.createElement("a");
                            link.href = data;
                            link.target = '_blank';
                            document.body.appendChild(link);
                            link.click();
                            document.body.removeChild(link);

                            document.getElementById('link').value = "";
                            document.getElementById('tamanho').value = "";
                            document.getElementById('xpe').value = "";
                            document.getElementById('xdeitada').value = "";
                            document.getElementById('ype').value = "";
                            document.getElementById('ydeitada').value = "";
                            document.getElementById('nomeArquivo').value = "";
                            document.getElementById('arquivo').value = "";

                            let loader = document.getElementById("loaderTesteMarca");
                            loader.style.display = 'none';
                        }
                        else{
                            Retorno.TrataRetorno(xhr, this._rotas);

                            let loader = document.getElementById("loaderTesteMarca");
                            loader.style.display = 'none';
                        }                        
                    });
                }
            }
        },

        validaCampos(){
            let link = document.getElementById('link').value;
            let tamanho = document.getElementById('tamanho').value;
            let xpe = document.getElementById('xpe').value;
            let xdeitada = document.getElementById('xdeitada').value;
            let ype = document.getElementById('ype').value;
            let ydeitada = document.getElementById('ydeitada').value;
            let arquivo = document.getElementById('arquivo').files.length;

            if(link.trim() == ""){
                $('.toast').hide();
                M.toast({html: 'O campo Link é obrigatório.', classes: 'rounded'});
                return false;
            }
            else if(isNaN(tamanho.toString().replace(",", ".")) || tamanho.trim() == ""){
                $('.toast').hide();
                M.toast({html: 'O campo Tamanho é obrigatório e deve ter um valor numérico.', classes: 'rounded'});
                return false;
            }
            else if(isNaN(xpe.toString().replace(",", ".")) || xpe.trim() == ""){
                $('.toast').hide();
                M.toast({html: 'O campo X Folha em pé é obrigatório e deve ter um valor numérico.', classes: 'rounded'});
                return false;
            }
            else if(isNaN(xdeitada.toString().replace(",", ".")) || xdeitada.trim() == ""){
                $('.toast').hide();
                M.toast({html: 'O campo X Folha deitada é obrigatório e deve ter um valor numérico.', classes: 'rounded'});
                return false;
            }
            else if(isNaN(ype.toString().replace(",", ".")) || ype.trim() == ""){
                $('.toast').hide();
                M.toast({html: 'O campo Y Folha em pé é obrigatório e deve ter um valor numérico.', classes: 'rounded'});
                return false;
            }
            else if(isNaN(ydeitada.toString().replace(",", ".")) || ydeitada.trim() == ""){
                $('.toast').hide();
                M.toast({html: 'O campo Y Folha deitada é obrigatório e deve ter um valor numérico.', classes: 'rounded'});
                return false;
            }
            else if(arquivo == 0){
                $('.toast').hide();
                M.toast({html: 'É necessário ao menos 1 arquivo para realizar o upload.', classes: 'rounded'});
                return false;
            }
            else{
                return true;
            }
        }
    },

    data(){
        return{
            //Card branco
            tamanho: "col s12",
            headerCardCadastro: {
                titulo: "Teste QrCode"
            },
        }
    } 
}
</script>

<style scoped>
.row {
    margin-bottom: 0px;
}
#loaderTesteQr {
    display: none;
}
</style>
