import React, {FC, useState} from 'react';
import {Layout, Menu, Tag} from 'antd';
import {
    AppstoreAddOutlined,
    BookOutlined,
    BoxPlotOutlined,
    LineChartOutlined,
    LogoutOutlined,
    QuestionOutlined,
    ScheduleOutlined,
    SettingOutlined,
    ShopOutlined,
    TeamOutlined,
    UserOutlined,
} from '@ant-design/icons';
import s from './AppMenu.module.css';
import {Link} from 'react-router-dom';
import {useAppDispatch, useAppSelector} from '../../../../store/store';
import {authActions} from '../../../../store/auth/auth.slice';
import {Role} from '../../modules/users/users.types';
import {isAdministrator} from '../../../../utils/permissions';

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
                    <Tag
                        color={'green'}>{me?.user?.role && Object.keys(Role)[Object.values(Role).indexOf(me.user.role)]}</Tag>
                </div>
            </div>
            <Menu theme="dark" /*defaultSelectedKeys={['1']}*/ mode="inline">
                <Menu.Item key="/teacher" icon={<LineChartOutlined/>}>
                    <Link to={'./'}>Головна</Link>
                </Menu.Item>
                <Menu.Item key="/subjects/my" icon={<BookOutlined/>}>
                    <Link to={'subjects/my'}>Мої предмети</Link>
                </Menu.Item>
                {isAdministrator() &&
                <SubMenu key="sub1" icon={<AppstoreAddOutlined/>} title="Сайт">
                    <Menu.Item key="sub1_1" icon={<QuestionOutlined/>}>Модуль 1</Menu.Item>
                    <Menu.Item key="sub1_2" icon={<QuestionOutlined/>}>Модуль 2</Menu.Item>
                </SubMenu>
                }
                <SubMenu key="sub2" icon={<ShopOutlined/>} title="Портал">
                    <Menu.Item key="subjects" icon={<BookOutlined/>}>
                        <Link to={'subjects'}>Предмети</Link>
                    </Menu.Item>
                    <Menu.Item key="grades" icon={<BoxPlotOutlined/>}>
                        <Link to={'grades'}>Класи</Link>
                    </Menu.Item>
                    <SubMenu key="sub3" icon={<TeamOutlined/>} title="Користувачі">
                        <Menu.Item key="students" icon={<UserOutlined/>}>
                            <Link to={'students'}>Учні</Link>
                        </Menu.Item>
                        <Menu.Item key="teachers" icon={<UserOutlined/>}>
                            <Link to={'teachers'}>Вчителі</Link>
                        </Menu.Item>
                    </SubMenu>
                    <Menu.Item key="educational-years" icon={<ScheduleOutlined/>}>
                        <Link to={'educational-years'}>Навчальні роки</Link>
                    </Menu.Item>
                </SubMenu>
                <Menu.Item key="settings" icon={<SettingOutlined/>}>
                    <Link to={'settings'}>Налаштування</Link>
                </Menu.Item>
                <Menu.Item key="site" icon={<UserOutlined/>}>
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
