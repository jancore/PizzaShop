import { App } from '../../module';

export class LoginCtrl {
    constructor(state, loginService) {
        this.loginService = loginService;
        this.state = state;
        this.user = {
            userName: null,
            password: null,
        };
    }
    login() {
        let self = this;
        this.loginService.logger(this.user)
            .then(function () {
                
                
            });
    }

}
LoginCtrl.$inject = ['$state', 'userService'];
App.controller('loginCtrl', LoginCtrl);