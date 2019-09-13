export default class MontaCores{

    static geraCor(){
        var hexadecimais = '0123456789ABCDEF';
        var cor = '#';
      
        for (var i = 0; i < 6; i++ ) {
            cor += hexadecimais[Math.floor(Math.random() * 16)];
        }
        return cor;
    }
}