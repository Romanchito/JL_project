import React from 'react';
import './App.css';
import './styles/home_film_block.css';
import './styles/film_inform_styles.css';
import './styles/login_style.css';
import './config.json';


import { Home } from './components/home';
import { Login } from './components/logining';
import { About } from './components/about';
import { Film } from './components/film';

import AuthComponent from './components/authenticate';

import { NavigationMenu } from './components/navigationMenu';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { Registration } from './components/registration';
import { UpdateUser } from './components/update_user';
import {RefreshPassword} from './components/updateUserPassword';
import UserAccount from './components/userAccountPage';



function App() {
  return (
    <div className="App">
      <NavigationMenu />
      <BrowserRouter>      
        <Switch>        
          <Route path="/log" component={Login} />
          <Route path="/" component={Home} exact />
          <Route path="/register" component={Registration} />
          <AuthComponent>
            <Route path="/refresh_password/:id" component={RefreshPassword} />
            <Route path="/update_user_inform/:id" component={UpdateUser} />
            <Route path="/user" component={UserAccount} />
            <Route path="/about" component={About} />
            <Route path="/film/:id" component={Film} />
          </AuthComponent>

        </Switch>
      </BrowserRouter>
      
    </div>

  );
}

export default App;
