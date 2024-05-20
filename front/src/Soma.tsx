import { useState } from "react";

//Como pegar os valores de uma caixa de texto em React
//Como mostrar o valor de uma variavel no HTML
function Soma() {

    const [contador, setContador] = useState(0);
    const [numero1, setNumero1] = useState("");
    const [numero2, setNumero2] = useState("");
    const [resultado, setResultado] = useState(0);

    function incrementarContador() {
        setContador(contador + 1);
        console.log(contador);
    }

    // //Funções para outro metodo de criação

    // function digitarNumero1 (e : any) {
    //     setNumero1(e.target.value);
    // }

    // function digitarNumero2 (e : any) {
    //     setNumero2(e.target.value);
    // }

    function somar() {
        setResultado(parseInt(numero1) + parseInt(numero2));
    }

    return (
    <div id="soma">
        <label> Número 1: </label>
        <input type="text" 
        onChange={(e : any) => {setNumero1(e.target.value)}}/>
        <br/>
        <label> Número 2: </label>
        <input type="text" 
        onChange={(e : any) => {setNumero2(e.target.value)}}/>
        <br/>
        <button onClick={incrementarContador}> Contador </button>
        <button onClick={somar}> Somar </button>
        <p> {contador} </p>
        <p> {resultado} </p>
    </div>
    );
}

export default Soma;