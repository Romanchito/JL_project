import React from 'react';
import './App.css';
import './styles/home_film_block.css';

import {Home} from './components/home';
import {Logining} from './components/logining';
import {About} from './components/about';
import { Film } from './components/film';

import {NavigationMenu} from './components/navigationMenu';

import {BrowserRouter, Route, Switch} from 'react-router-dom'


function App() {
  return (
    <div className="App">
      
      
      <BrowserRouter>
      <header className="App-header">
      <NavigationMenu />
      </header>
      
          <Switch>
            <Route path="/" component={Home} exact />
            <Route path="/about" component={About} />
            <Route path="/log" component={Logining}/>
            <Route path="/film/:id" children={<Film />}/>
          </Switch>
        </BrowserRouter>      
    </div>
    
  );
}

export default App;
