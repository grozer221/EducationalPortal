import React, {FC} from 'react';
import {Link} from 'react-router-dom';
import {useAppDispatch, useAppSelector} from '../../../store/store';
import {Role} from '../../../gql/modules/users/users.types';
import Title from 'antd/es/typography/Title';
import {Button, Col, Row} from 'antd';
import {authActions} from '../../../store/auth.slice';
import {AppName, AppNameType} from '../../../store/settings.slice';

export const ClientLayout: FC = () => {
    const dispatch = useAppDispatch();
    const isAuth = useAppSelector(s => s.auth.isAuth);
    const me = useAppSelector(s => s.auth.me);
    const settings = useAppSelector(s => s.settings.settings);

    return (
        <Row justify={'center'} align={'middle'} style={{height: '100vh'}}>
            <Col>
                <Title>{(settings.find(s => s.name === AppName) as AppNameType).value}</Title>
                <Title level={2}>Тут скоро буде крутий сайт :)</Title>
                {isAuth && <Title level={3}>Hey, {me?.user.lastName} {me?.user.firstName}</Title>}
                {isAuth && <Button onClick={() => dispatch(authActions.logout())}>Вийти</Button>}
                <Title level={4}>
                    {isAuth
                        ? <Link to={me?.user.role === Role.Student ? '/student' : '/teacher'}>Портал</Link>
                        : <Link to={'/login'}>Вхід</Link>
                    }
                </Title>
            </Col>
        </Row>
    );
};
