import { App } from '../../module';

export class AddUserCtrl {
    constructor(state, userService) {
        this.userService = userService;
        this.state = state;
        this.user = {
            email: null,
            password: null,
            passwordAgain: null,
            userName: null
        };
    }
    saveUser() {
        let self = this;
        this.userService.create(this.user)
            .then(function () {
                $http({
                    method: 'POST',
                    url: 'http://localhost:51889',
                    data: JSON.stringify(this.user)
                });
            });
    }

}
AddUserCtrl.$inject = ['$state', 'userService'];
App.controller('addUserCtrl', AddUserCtrl);