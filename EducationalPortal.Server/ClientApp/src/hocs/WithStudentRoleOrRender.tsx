import React, {FC} from 'react';
import {useAppSelector} from '../store/store';
import {Role} from '../areas/teacher-area/modules/users/users.types';

type Props = {
    render: React.ReactNode;
}

export const WithStudentRoleOrRender: FC<Props> = ({children, render}) => {
    const me = useAppSelector(s => s.auth.me);
    if (me?.user.role !== Role.Student && me?.user.role !== Role.Teacher && me?.user.role !== Role.Administrator)
        return <>{render}</>;
    return <>{children}</>;
};
