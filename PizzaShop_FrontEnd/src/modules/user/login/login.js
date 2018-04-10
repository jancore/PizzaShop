import { App } from '../../module';
import {} from './config';
import {} from './loginctrl';
import html from './login.html';

export const ilLogin = {
    controller: 'loginCtrl',
    template: html
};
App.component('ilLogin', ilLogin)
