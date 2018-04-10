import { App } from '../../module';

export class AddUserCtrl {
    constructor(state, userService) {
        this.userService = userService;
        this.state = state;
        this.user = {
            userName: null,
            email: null,
            password: null,
            confirmPassword: null
        };
    }
    saveUser() {
        let self = this;
        this.userService.create(this.user)
            .then(function () {
                
                
            });
    }

}
AddUserCtrl.$inject = ['$state', 'userService'];
App.controller('addUserCtrl', AddUserCtrl);