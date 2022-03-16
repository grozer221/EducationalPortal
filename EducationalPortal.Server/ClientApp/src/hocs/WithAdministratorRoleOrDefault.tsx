import React, {FC} from 'react';
import {useAppSelector} from '../store/store';
import {Role} from '../gql/modules/users/users.types';

export const WithAdministratorRoleOrDefault: FC = ({children}) => {
    const me = useAppSelector(s => s.auth.me);
    if (me?.user.role !== Role.Administrator)
        return (<></>);
    return (<>{children}</>);
};
