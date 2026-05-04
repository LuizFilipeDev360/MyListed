import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { MediaComponent } from './components/media/media.component';
import { MyListComponent } from './components/my-list/my-list.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ProfileSettingsComponent } from './components/profile-settings/profile-settings.component';
import { roleGuard } from './guards/role.guard';
import { RegisterComponent } from './components/register/register.component';
import { ResultsComponent } from './components/results/results.component';
import { ManagerComponent } from './components/manager/manager.component';

export const routes: Routes = [
    {
        path: '',
        component: HomeComponent
    },{
        path: 'home',
        component: HomeComponent
    },
    {
        path: 'results',
        component: ResultsComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'mylist',
        component: MyListComponent,
        canActivate: [roleGuard],
        data: { roles: ['Admin', 'Manager', 'User'] }
    },
    {
        path: 'media/:id',
        component: MediaComponent
    },
    {
        path: 'profile',
        component: ProfileComponent,
        canActivate: [roleGuard],
        data: { roles: ['Admin', 'Manager', 'User'] }
    },
    {
        path: 'profile/settings',
        component: ProfileSettingsComponent,
        canActivate: [roleGuard],
        data: { roles: ['Admin', 'Manager', 'User'] }
    },
    {
        path: 'manager',
        component: ManagerComponent,
        canActivate: [roleGuard],
        data: { roles: ['Admin', 'Manager'] }
    }
];
