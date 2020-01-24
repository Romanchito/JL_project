import React from 'react';
import './App.css';
import './styles/home_film_block.css';
import './styles/film_inform_styles.css';
import './styles/login_style.css';
import './styles/status_code_styles.css'
import './config.json';


import { Home } from './components/home';
import { Login } from './components/logining';
import { About } from './components/about';
import { Film } from './components/film';

import AuthComponent from './components/authenticate';

import NavigationMenu from './components/navigationMenu';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { Registration } from './components/registration';
import { UpdateUser } from './components/update_user';
import { RefreshPassword } from './components/updateUserPassword';
import UserAccount from './components/userAccountPage';
import { UpdateUserImage } from './components/updateUserImage';
import { MainReviewInform } from './components/fullReviewInform';
import { ErrorPage } from './components/helpers/serverErrorPage';





function App() {
  return (
    <div className="App">      
      <BrowserRouter>
      <NavigationMenu />
          <Switch>
          <Route path="/log" component={Login} />
          <Route path="/" component={Home} exact />
          <Route path="/register" component={Registration} />
          <Route path="/review/:id" component={MainReviewInform}/>
          <Route path="/error/:code" component={ErrorPage}/>
          <Route path="/film/:id" component={Film} />
          <AuthComponent>
            <Route path="/uploadImage" component={UpdateUserImage}/>
            <Route path="/refresh_password/:id" component={RefreshPassword} />
            <Route path="/update_user_inform/:id" component={UpdateUser} />
            <Route path="/user" component={UserAccount} />
            <Route path="/about" component={About} />                      
          </AuthComponent>
          

        </Switch>
      </BrowserRouter>

    </div>

  );
}

export default App;
