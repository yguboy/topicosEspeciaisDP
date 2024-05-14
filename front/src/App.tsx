import React from 'react';
import Soma from './Soma';

//1 - Um componente sempre deve começar com a primeira letra Maiúscula

//2 - Todo componente deve ser uma função do JS

//3 - Todo componente deve retornar apenas um elemento HTML
function App() {
  return (
    <div>
      <Soma></Soma>
    </div>
  );
}
//4 - Obrigatoriamente o componente deve ser exportado
export default App;
