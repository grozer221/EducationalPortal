import React, {useEffect, useState} from 'react';
import {Route, Routes} from 'react-router-dom';
import {authActions} from './store/auth/auth.slice';
import {Loading} from './components/Loading/Loading';
import {TeacherLayout} from './areas/teacher-area/TeacherLayout/TeacherLayout';
import {LoginPage} from './pages/LoginPage/LoginPage';
import {useAppDispatch} from './store/store';
import {WithTeacherRole} from './hocs/WithTeacherRole';
import {ClientLayout} from './areas/client-area/ClientLayout/ClientLayout';
import {useQuery} from '@apollo/client';
import {ME_QUERY, MeData, MeVars} from './gql/auth/auth.queries';
import 'antd/dist/antd.css';
import './App.css';

export const App = () => {
    const dispatch = useAppDispatch();
    const meQuery = useQuery<MeData, MeVars>(ME_QUERY);
    const [isInitialised, setIsInitialised] = useState(false);

    useEffect(() => {
        if (meQuery.data) {
            dispatch(authActions.login({me: meQuery.data.me}));
            setIsInitialised(true);
        }
        if (meQuery.error) {
            setIsInitialised(true);
        }
    }, [meQuery]);

    if (meQuery.loading || !isInitialised)
        return <Loading/>;

    return (
        <Routes>
            <Route path="login" element={<LoginPage/>}/>
            <Route path="teacher/*" element={<WithTeacherRole element={<TeacherLayout/>}/>}/>
            <Route path="student/*" element={<div>StudentLayout</div>}/>
            <Route path="/*" element={<ClientLayout/>}/>
        </Routes>
    );
};
