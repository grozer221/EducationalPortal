import React, {FC, useState} from 'react';
import {Col, Layout, Menu, Row} from 'antd';
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
import {Link, useLocation} from 'react-router-dom';
import {useAppDispatch, useAppSelector} from '../../../../store/store';
import {authActions} from '../../../../store/auth.slice';
import {isAdministrator} from '../../../../utils/permissions';
import {AppName, AppNameType} from '../../../../store/settings.slice';
import Title from 'antd/es/typography/Title';
import {roleToTag} from '../../../../convertors/enumToTagConvertor';

const {Sider} = Layout;
const {SubMenu} = Menu;

export const AppMenu: FC = () => {
    const [collapsed, setCollapsed] = useState(false);
    const me = useAppSelector(state => state.auth.me);
    const dispatch = useAppDispatch();
    const settings = useAppSelector(s => s.settings.settings);
    const [startUrl, setStartUrl] = useState(useLocation().pathname.replace('/teacher/', ''));

    const getDefaultSelectedKey = (): string => {
        if(startUrl.match(/subjects\/my/))
            return 'subjects/my';
        else if(startUrl.match(/subjects/))
            return 'subjects';
        else if(startUrl.match(/grades/))
            return 'grades';
        else if(startUrl.match(/students/))
            return 'students';
        else if(startUrl.match(/teachers/))
            return 'teachers';
        else if(startUrl.match(/educational-years/))
            return 'educational-years';
        else if(startUrl.match(/settings\/my/))
            return 'settings/my';
        else if(startUrl.match(/settings\/site/))
            return 'settings/site';
        else
            return '';
    }

    const getDefaultOpenKeys = (): string[] => {
        const defaultOpenKeys = [];
        if((!startUrl.match(/subjects\/my/) && startUrl.match(/subjects|grades|educational-years|students|teachers/)))
            defaultOpenKeys.push('portal')
        if(startUrl.match(/students|teachers/))
            defaultOpenKeys.push('users')
        if(startUrl.match(/settings/))
            defaultOpenKeys.push('settings')
        return defaultOpenKeys
    }

    return (
        <Sider collapsible collapsed={collapsed} onCollapse={setCollapsed} className={s.wrapperMenu}>
            <Row justify="center" style={{marginTop: '20px', textAlign: 'center'}}>
                <Col>
                    <Title level={3} style={{color: '#3498db'}}>
                        {(settings.find(s => s.name === AppName) as AppNameType)?.value}
                    </Title>
                </Col>
                <Col>
                    <span className={s.name}>{me?.user.firstName} </span>
                    <span className={s.name}>{me?.user.lastName}</span>
                </Col>
                <Col>
                    {me?.user?.role && roleToTag(me.user.role)}
                </Col>
            </Row>
            <Menu theme="dark" defaultSelectedKeys={[getDefaultSelectedKey()]} defaultOpenKeys={getDefaultOpenKeys()} mode="inline">
                <Menu.Item key="/" icon={<LineChartOutlined/>}>
                    <Link to={'./'}>Головна</Link>
                </Menu.Item>
                <Menu.Item key="subjects/my" icon={<BookOutlined/>}>
                    <Link to={'subjects/my'}>Мої предмети</Link>
                </Menu.Item>
                {isAdministrator() &&
                <SubMenu key="sub1" icon={<AppstoreAddOutlined/>} title="Сайт">
                    <Menu.Item key="sub1_1" icon={<QuestionOutlined/>}>Модуль 1</Menu.Item>
                    <Menu.Item key="sub1_2" icon={<QuestionOutlined/>}>Модуль 2</Menu.Item>
                </SubMenu>
                }
                <SubMenu key="portal" icon={<ShopOutlined/>} title="Портал">
                    <Menu.Item key="subjects" icon={<BookOutlined/>}>
                        <Link to={'subjects'}>Предмети</Link>
                    </Menu.Item>
                    <Menu.Item key="homeworks" icon={<BookOutlined/>}>
                        <Link to={'homeworks'}>Домашні роботи</Link>
                    </Menu.Item>
                    <Menu.Item key="grades" icon={<BoxPlotOutlined/>}>
                        <Link to={'grades'}>Класи</Link>
                    </Menu.Item>
                    <SubMenu key="users" icon={<TeamOutlined/>} title="Користувачі">
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
                {isAdministrator()
                    ? <SubMenu key="settings" icon={<SettingOutlined/>} title="Налаштування">
                        <Menu.Item key="settings/my" icon={<SettingOutlined/>}>
                            <Link to={'settings/my'}>Мої</Link>
                        </Menu.Item>
                        <Menu.Item key="settings/site" icon={<SettingOutlined/>}>
                            <Link to={'settings/site'}>Сайту</Link>
                        </Menu.Item>
                    </SubMenu>
                    : <Menu.Item key="settings/my" icon={<SettingOutlined/>}>
                        <Link to={'settings/my'}>Налаштування</Link>
                    </Menu.Item>
                }
                <Menu.Item key="site" icon={<UserOutlined/>}>
                    <Link to={'/'}>На сайт</Link>
                </Menu.Item>
                <Menu.Item key="logout" icon={<LogoutOutlined/>} onClick={() => dispatch(authActions.logout())}>
                    Вийти
                </Menu.Item>
                <div style={{height: '48px'}}/>
            </Menu>;
        </Sider>
    );
};
