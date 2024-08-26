import { environment } from '../../../environments/environment';

export const ApiRoutes = {
    Auth: {
        Token: `${environment.API_BASEURL}/token`
    },
    Organizations: {
        GetOrganizationList: `${environment.API_BASEURL}/organizations/list`
    }
}

export const ApplicationRoutes = {
    Login: '/login',
    Dashboard: '/dashboard',
}