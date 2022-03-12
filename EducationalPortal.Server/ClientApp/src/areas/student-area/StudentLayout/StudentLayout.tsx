import React, {FC} from 'react';
import {Layout} from 'antd';
import {Route, Routes} from 'react-router-dom';
import {AppBreadcrumb} from '../../teacher-area/components/AppBreadcrumb/AppBreadcrumb';
import {Error} from '../../../components/Error/Error';
import s from './StudentLayout.module.css';
import {StudentMenu} from '../components/StudentMenu/StudentMenu';

const {Content} = Layout;

export const StudentLayout: FC = () => {
    return (
        <Layout className={s.layout}>
            <Layout className="site-layout">
                <Content className={s.content}>
                    <AppBreadcrumb/>
                    <div className={s.siteLayoutBackground}>
                        <StudentMenu/>
                        <Routes>
                            <Route path={'subjects/*'}>
                                <Route path={'my'} element={<div>SubjectsMyIndex</div>}/>
                                <Route path={':id'} element={<div>id</div>}/>
                                <Route path={'*'} element={<Error/>}/>
                            </Route>
                            <Route path={'settings/*'} element={<div>settings</div>}/>
                            <Route path={'*'} element={<Error/>}/>
                        </Routes>
                    </div>
                </Content>
            </Layout>
        </Layout>
    );
};
