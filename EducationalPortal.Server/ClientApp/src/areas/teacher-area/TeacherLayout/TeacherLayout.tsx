import React, {FC} from 'react';
import {Layout} from 'antd';
import {Route, Routes} from 'react-router-dom';
import {AppMenu} from '../components/AppMenu/AppMenu';
import {AppBreadcrumb} from '../components/AppBreadcrumb/AppBreadcrumb';
import {EducationalYearsLayout} from '../modules/educationalYears/EducationalYearsLayout/EducationalYearsLayout';
import {Error} from '../../../components/Error/Error';
import s from './TeacherLayout.module.css';
import {SubjectsLayout} from '../modules/subjects/SubjectsLayout/SubjectsLayout';
import {GradesLayout} from '../modules/grades/GradesLayout/GradesLayout';
import {StudentsLayout} from '../modules/users/StudentsLayout/StudentsLayout';
import {TeachersLayout} from '../modules/users/TeachersLayout/TeachersLayout';

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
                            <Route path={'educational-years/*'} element={<EducationalYearsLayout/>}/>
                            <Route path={'subjects/*'} element={<SubjectsLayout/>}/>
                            <Route path={'grades/*'} element={<GradesLayout/>}/>
                            <Route path={'students/*'} element={<StudentsLayout/>}/>
                            <Route path={'teachers/*'} element={<TeachersLayout/>}/>
                            <Route path={'*'} element={<Error/>}/>
                        </Routes>
                    </div>
                </Content>
            </Layout>
        </Layout>
    );
};
