import React, {FC} from 'react';
import {Route, Routes} from 'react-router-dom';
import {Error} from '../../../../../components/Error/Error';
import {StudentsIndex} from '../pages/StudentsIndex/StudentsIndex';
import {StudentsCreate} from '../pages/StudentsCreate/StudentsCreate';
import {StudentsView} from '../pages/StudentsView/StudentsView';
import {StudentsUpdate} from '../pages/StudentsUpdate/StudentsUpdate';
import {WithAdministratorRoleOrRender} from '../../../../../hocs/WithAdministratorRoleOrRender';

export const StudentsLayout: FC = () => {
    return (
        // <Routes>
        //     <Route path={'/'} element={<StudentsIndex/>}/>
        //     <Route path={':id'} element={<StudentsView/>}/>
        //     <Route path={'create'} element={
        //         <WithAdministratorRoleOrRender render={<Error statusCode={403}/>}>
        //             <StudentsCreate/>
        //         </WithAdministratorRoleOrRender>
        //     }/>
        //     <Route path={'update/:id'} element={
        //         <WithAdministratorRoleOrRender render={<Error statusCode={403}/>}>
        //             <StudentsUpdate/>
        //         </WithAdministratorRoleOrRender>
        //     }/>
        //     <Route path={'*'} element={<Error/>}/>
        // </Routes>
        <Routes>
            <Route path={'/'} element={<StudentsIndex/>}/>
            <Route path={':id'} element={<StudentsView/>}/>
            <Route path={'create'} element={
                    <StudentsCreate/>
            }/>
            <Route path={'update/:id'} element={
                    <StudentsUpdate/>
            }/>
            <Route path={'*'} element={<Error/>}/>
        </Routes>
    );
};
