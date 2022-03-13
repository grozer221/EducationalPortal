import React, {useEffect, useState} from 'react';
import {Navigate, Route, Routes} from 'react-router-dom';
import {authActions} from './store/auth.slice';
import {Loading} from './components/Loading/Loading';
import {TeacherLayout} from './areas/teacher-area/TeacherLayout/TeacherLayout';
import {LoginPage} from './gql/auth/pages/LoginPage/LoginPage';
import {useAppDispatch, useAppSelector} from './store/store';
import {WithTeacherRoleOrRender} from './hocs/WithTeacherRoleOrRender';
import {ClientLayout} from './areas/client-area/ClientLayout/ClientLayout';
import {ME_QUERY, MeData, MeVars} from './gql/auth/auth.queries';
import 'antd/dist/antd.css';
import './App.css';
import {
    GET_SETTINGS_QUERY,
    GetSettingsData,
    GetSettingsVars,
} from './areas/teacher-area/modules/settings/settings.queries';
import {client} from './gql/client';
import {AppName, settingsActions} from './store/settings.slice';
import {WithStudentRoleOrRender} from './hocs/WithStudentRoleOrRender';
import {StudentLayout} from './areas/student-area/StudentLayout/StudentLayout';
import {Error} from './components/Error/Error';

export const App = () => {
    const dispatch = useAppDispatch();
    const [isMeDone, setMeDone] = useState(false);
    const [isGetSettingsDone, setIsGetSettingsDone] = useState(false);
    const isAuth = useAppSelector(s => s.auth.isAuth);

    useEffect(() => {
        client.query<MeData, MeVars>({query: ME_QUERY})
            .then(response => {
                dispatch(authActions.login({me: response.data.me}));
                setMeDone(true);
            })
            .catch(error => {
                setMeDone(true);
            });

        client.query<GetSettingsData, GetSettingsVars>({query: GET_SETTINGS_QUERY})
            .then(response => {
                document.title = response.data.getSettings.find(s => s.name === AppName)?.value;
                dispatch(settingsActions.setSettings(response.data.getSettings));
                setIsGetSettingsDone(true);
            })
            .catch(error => {
                setIsGetSettingsDone(true);
            });
    }, []);

    const settings = useAppSelector(s => s.settings.settings);
    console.log(settings);

    if (!isMeDone || !isGetSettingsDone)
        return <Loading/>;

    return (
        <Routes>
            <Route path="login" element={<LoginPage/>}/>
            <Route path="teacher/*" element={
                <WithTeacherRoleOrRender render={isAuth ? <Error statusCode={403}/> : <Navigate to={'/login'}/>}>
                    <TeacherLayout/>
                </WithTeacherRoleOrRender>
            }/>
            <Route path="student/*" element={
                <WithStudentRoleOrRender render={isAuth ? <Error statusCode={403}/> : <Navigate to={'/login'}/>}>
                    <StudentLayout/>
                </WithStudentRoleOrRender>
            }/>
            <Route path="/*" element={<ClientLayout/>}/>
        </Routes>
    );
};
