import React, {FC} from 'react';
import {Route, Routes} from 'react-router-dom';
import {Error} from '../../../../../components/Error/Error';
import {TeachersIndex} from '../pages/TeachersIndex/TeachersIndex';
import {TeachersCreate} from '../pages/TeachersCreate/TeachersCreate';
import {TeachersView} from '../pages/TeachersView/TeachersView';
import {TeachersUpdate} from '../pages/TeachersUpdate/TeachersUpdate';

export const TeachersLayout: FC = () => {
    return (
        // <Routes>
        //     <Route path={'/'} element={<TeachersIndex/>}/>
        //     <Route path={':id'} element={<TeachersView/>}/>
        //     <Route path={'create'} element={
        //         <WithAdministratorRoleOrRender render={<Error statusCode={403}/>}>
        //             <TeachersCreate/>
        //         </WithAdministratorRoleOrRender>
        //     }/>
        //     <Route path={'update/:id'} element={
        //         <WithAdministratorRoleOrRender render={<Error statusCode={403}/>}>
        //             <TeachersUpdate/>
        //         </WithAdministratorRoleOrRender>
        //     }/>
        //     <Route path={'*'} element={<Error/>}/>
        // </Routes>
        <Routes>
            <Route path={'/'} element={<TeachersIndex/>}/>
            <Route path={':id'} element={<TeachersView/>}/>
            <Route path={'create'} element={
                <TeachersCreate/>
            }/>
            <Route path={'update/:id'} element={
                <TeachersUpdate/>
            }/>
            <Route path={'*'} element={<Error/>}/>
        </Routes>
    );
};
