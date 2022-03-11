import React, {FC} from 'react';
import {Menu} from 'antd';
import {BookOutlined, LogoutOutlined, SettingOutlined, UserOutlined} from '@ant-design/icons';
import {Link} from 'react-router-dom';
import {authActions} from '../../../../store/auth.slice';
import {useAppDispatch} from '../../../../store/store';

export const StudentMenu: FC = () => {
    const dispatch = useAppDispatch();

    return (
        <Menu mode="horizontal">
            <Menu.Item key="/subjects/my" icon={<BookOutlined/>}>
                <Link to={'subjects/my'}>Мої предмети</Link>
            </Menu.Item>
            <Menu.Item key="settings/my" icon={<SettingOutlined/>}>
                <Link to={'settings/my'}>Налаштування</Link>
            </Menu.Item>
            <Menu.Item key="site" icon={<UserOutlined/>}>
                <Link to={'/'}>На сайт</Link>
            </Menu.Item>
            <Menu.Item key="140" icon={<LogoutOutlined/>} onClick={() => dispatch(authActions.logout())}>
                Вийти
            </Menu.Item>
        </Menu>
    );
};
