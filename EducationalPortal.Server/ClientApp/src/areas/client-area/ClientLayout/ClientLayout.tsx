import React, {FC} from 'react';
import {Link} from 'react-router-dom';
import {authActions} from '../../../store/auth.slice';
import {useAppDispatch, useAppSelector} from '../../../store/store';

export const ClientLayout: FC = () => {
    const dispatch = useAppDispatch();
    const isAuth = useAppSelector(s => s.auth.isAuth);

    return (
        <div>
            <div>Client layout</div>
            <div>
                <Link to={'/student'}>Student</Link>
            </div>
            <div>
                <Link to={'/teacher'}>Teacher</Link>
            </div>
            {isAuth && <button onClick={() => dispatch(authActions.logout())}>
                Вийти
            </button>}
        </div>
    );
};
