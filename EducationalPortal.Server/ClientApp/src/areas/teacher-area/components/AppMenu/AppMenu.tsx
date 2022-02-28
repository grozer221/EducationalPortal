import React, {FC, useState} from 'react';
import {Layout, Menu, Tag} from 'antd';
import {
    AppstoreAddOutlined,
    LineChartOutlined,
    LogoutOutlined,
    QuestionOutlined,
    SettingOutlined,
    TeamOutlined,
    UserOutlined,
} from '@ant-design/icons';
import s from './AppMenu.module.css';
import {Link} from 'react-router-dom';
import {useAppDispatch, useAppSelector} from '../../../../store/store';
import {authActions} from '../../../../store/auth/auth.slice';

const {Sider} = Layout;
const {SubMenu} = Menu;


export const AppMenu: FC = () => {
    const [collapsed, setCollapsed] = useState(false);
    const me = useAppSelector(state => state.auth.me);
    const dispatch = useAppDispatch();

    return (
        <Sider collapsible collapsed={collapsed} onCollapse={setCollapsed} className={s.wrapperMenu}>
            <div className={s.logo}/>
            <div className={s.userInfo}>
                <div className={s.userFirstLastName}>
                    <span className={s.name}>{me?.user.firstName}</span>
                    <span className={s.name}>{me?.user.lastName}</span>
                </div>
                <div className={s.roles}>
                    <Tag color={'green'}>{me?.user.role}</Tag>
                </div>
            </div>
            <Menu theme="dark" /*defaultSelectedKeys={['1']}*/ mode="inline">
                <Menu.Item key="10" icon={<LineChartOutlined/>}>
                    <Link to={'./'}>Головна</Link>
                </Menu.Item>
                <SubMenu key="sub1" icon={<AppstoreAddOutlined/>} title="Сайт">
                    <Menu.Item key="sub1_1" icon={<QuestionOutlined/>}>Модуль 1</Menu.Item>
                    <Menu.Item key="sub1_2" icon={<QuestionOutlined/>}>Модуль 2</Menu.Item>
                </SubMenu>
                <Menu.Item key="90" icon={<TeamOutlined/>}>
                    <Link to={'users'}>Користувачі</Link>
                </Menu.Item>
                <Menu.Item key="120" icon={<SettingOutlined/>}>
                    <Link to={'settings'}>Налаштування</Link>
                </Menu.Item>
                <Menu.Item key="130" icon={<UserOutlined/>}>
                    <Link to={'/'}>На сайт</Link>
                </Menu.Item>
                <Menu.Item key="140" icon={<LogoutOutlined/>} onClick={() => dispatch(authActions.logout())}>
                    Вийти
                </Menu.Item>
                <div style={{height: '48px'}}/>
            </Menu>
        </Sider>
    );
};
