import {store} from '../store/store';
import {Role} from '../areas/teacher-area/modules/users/users.types';

export const isTeacher = (): boolean => {
    return store.getState().auth.me?.user.role === Role.Teacher;
};

export const isAdministrator = (): boolean => {
    return store.getState().auth.me?.user.role === Role.Administrator;
};
