import React from 'react';
import './App.css';
import './styles/home_film_block.css';
import './styles/film_inform_styles.css';
import './styles/login_style.css';
import './config.json';


import {Home} from './components/home';
import {Login} from './components/logining';
import {About} from './components/about';
import { Film } from './components/film';

import {NavigationMenu} from './components/navigationMenu';
import {BrowserRouter, Route, Switch} from 'react-router-dom';



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
            <Route path="/log" component={Login}/>
            <Route path="/film/:id" component={Film}/>
          </Switch>
        </BrowserRouter>      
    </div>
    
  );
}

export default App;