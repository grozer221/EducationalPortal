import React, {FC} from 'react';
import {Route, Routes} from 'react-router-dom';
import {Error} from '../../../../../components/Error/Error';
import {GradesIndex} from '../pages/GradesIndex/GradesIndex';
import {GradesCreate} from '../pages/GradesCreate/GradesCreate';
import {GradesView} from '../pages/GradesView/GradesView';
import {GradesUpdate} from '../pages/GradesUpdate/GradesUpdate';
import {WithAdministratorRoleOrRender} from '../../../../../hocs/WithAdministratorRoleOrRender';

export const GradesLayout: FC = () => {
    return (
        <Routes>
            <Route path={'/'} element={<GradesIndex/>}/>
            <Route path={':id'} element={<GradesView/>}/>
            <Route path={'create'} element={
                <WithAdministratorRoleOrRender render={<Error statusCode={403}/>}>
                    <GradesCreate/>
                </WithAdministratorRoleOrRender>
            }/>
            <Route path={'update/:id'} element={
                <WithAdministratorRoleOrRender render={<Error statusCode={403}/>}>
                    <GradesUpdate/>
                </WithAdministratorRoleOrRender>
            }/>
            <Route path={'*'} element={<Error/>}/>
        </Routes>
    );
};
