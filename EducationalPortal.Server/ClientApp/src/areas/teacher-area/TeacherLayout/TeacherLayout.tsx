import React, {FC} from 'react';
import {Layout} from 'antd';
import {Route, Routes} from 'react-router-dom';
import s from './TeacherLayout.module.css';
import {Error} from '../../../components/Error/Error';
import {AppMenu} from '../components/AppMenu/AppMenu';
import {AppBreadcrumb} from '../components/AppBreadcrumb/AppBreadcrumb';

const {Content} = Layout;

export const TeacherLayout: FC = () => {
    return (
        <Layout className={s.layout}>
            <AppMenu/>
            <Layout className="site-layout">
                <Content className={s.content}>
                    <AppBreadcrumb/>
                    <div className={s.siteLayoutBackground}>
                        <Routes>
                            <Route path={'/'} element={<div>home</div>}/>
                            <Route path={'*'} element={<Error/>}/>
                        </Routes>
                    </div>
                </Content>
            </Layout>
        </Layout>
    );
};
