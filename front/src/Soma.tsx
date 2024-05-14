import { useState } from "react";

//Como pegar os valores de uma caixa de texto em React
//Como mostrar o valor de uma variavel no HTML
function Soma() {
    const [contador, setContador] = useState(0);

    function clicar() {
        setContador(contador + 1);
        console.log(contador);
    }

    return (
    <div>
        <label> Número 1: </label>
        <input type="text"/>
        <label> Número 2: </label>
        <input type="text"/>
        <button onClick={clicar}> Somar </button>
    </div>
    );
}

export default Soma;