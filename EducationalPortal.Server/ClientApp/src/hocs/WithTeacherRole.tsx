import React from 'react';
import {useAppSelector} from '../store/store';
import {Role} from '../areas/teacher-area/modules/users/users.types';
import {Navigate} from 'react-router-dom';

type Props = {
    element: React.ReactNode;
};

export const WithTeacherRole = (props: Props) => {
    const me = useAppSelector(s => s.auth.me);

    if (me?.user.role !== Role.Teacher && me?.user.role !== Role.Administrator)
        return <Navigate to={'/login'}/>;

    return <>{props.element}</>;
};
