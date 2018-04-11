import { App } from '../../module';

export class LoginCtrl {
    constructor(state, userService) {
        this.userService = userService;
        this.state = state;
        this.user = {
            userName: null,
            password: null,
            grant_type: "password",
        };
    }
    login() {
        let self = this;
        this.userService.logger(this.user)
            .then(function () {    
            });
    }

}
LoginCtrl.$inject = ['$state', 'userService'];
App.controller('loginCtrl', LoginCtrl);