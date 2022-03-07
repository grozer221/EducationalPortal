import React, {FC} from 'react';
import {Route, Routes} from 'react-router-dom';
import {EducationalYearsIndex} from '../pages/EducationalYearsIndex/EducationalYearsIndex';
import {EducationalYearsView} from '../pages/EducationalYearsView/EducationalYearsView';
import {Error} from '../../../../../components/Error/Error';
import {EducationalYearsUpdate} from '../pages/EducationalYearsUpdate/EducationalYearsUpdate';
import {EducationalYearsCreate} from '../pages/EducationalYearsCreate/EducationalYearsCreate';
import {WithAdministratorRoleOrRender} from '../../../../../hocs/WithAdministratorRoleOrRender';

export const EducationalYearsLayout: FC = () => {
    return (
        <Routes>
            <Route path={'/'} element={<EducationalYearsIndex/>}/>
            <Route path={':id'} element={<EducationalYearsView/>}/>
            <Route path={'create'} element={
                <WithAdministratorRoleOrRender render={<Error statusCode={403}/>}>
                    <EducationalYearsCreate/>
                </WithAdministratorRoleOrRender>}/>
            <Route path={'update/:id'} element={
                <WithAdministratorRoleOrRender render={<Error statusCode={403}/>}>
                    <EducationalYearsUpdate/>
                </WithAdministratorRoleOrRender>
            }/>
            <Route path={'*'} element={<Error/>}/>
        </Routes>
    );
};
