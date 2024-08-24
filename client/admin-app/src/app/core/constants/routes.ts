import { environment } from '../../../environments/environment';

export const ApiRoutes = {
    getToken: `${environment.API_BASEURL}/token`
}

export const ApplicationRoutes = {
    Login: '/login',
    Dashboard: '/dashboard',
}