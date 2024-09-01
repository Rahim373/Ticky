import { environment } from '../../../environments/environment';

export const ApiRoutes = {
    Auth: {
        Token: `${environment.API_BASEURL}/token`,
    },
    Organizations: {
        Invitations: `${environment.API_BASEURL}/organizations/invitations`,
        GetOrganizationList: `${environment.API_BASEURL}/organizations/list`
    }
}

export const ApplicationRoutes = {
    Login: '/login',
    Dashboard: '/dashboard',
}