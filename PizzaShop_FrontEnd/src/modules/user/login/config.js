import { App } from '../../module';

config.$inject = ["$stateProvider"];
export function config(stateProvider) {
    stateProvider
        .state('login', {
            parent: 'app',
            url: 'users/login',
            component: 'ilLogin'
        });
}
App.config(config);  