import React, {FC} from 'react';
import {Link} from 'react-router-dom';
import {authActions} from '../../../store/auth.slice';
import {useAppDispatch, useAppSelector} from '../../../store/store';
import {Role} from '../../teacher-area/modules/users/users.types';

export const ClientLayout: FC = () => {
    const dispatch = useAppDispatch();
    const isAuth = useAppSelector(s => s.auth.isAuth);
    const auth = useAppSelector(s => s.auth);

    return (
        <div>
            <div>Client layout</div>
            {auth.isAuth ? (
                auth.me?.user.role === Role.Student
                    ? <div>
                        <Link to={'/student'}>Student</Link>
                    </div>
                    : <div>
                        <Link to={'/teacher'}>Teacher</Link>
                    </div>
            )
                : <Link to={'/login'}>login</Link>
            }

            {auth.isAuth && <div>Hey, {auth.me?.user.lastName} {auth.me?.user.firstName}</div>}
            {isAuth && <button onClick={() => dispatch(authActions.logout())}>
                Вийти
            </button>}
        </div>
    );
};
