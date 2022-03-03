import React, {FC} from 'react';
import {Route, Routes} from 'react-router-dom';
import {EducationalYearsIndex} from '../pages/EducationalYearsIndex/EducationalYearsIndex';
import {EducationalYearsView} from '../pages/EducationalYearsView/EducationalYearsView';
import {Error} from '../../../../../components/Error/Error';
import {EducationalYearsUpdate} from '../pages/EducationalYearsUpdate/EducationalYearsUpdate';
import {EducationalYearsCreate} from '../pages/EducationalYearsCreate/EducationalYearsCreate';

export const EducationalYearsLayout: FC = () => {
    return (
        <Routes>
            <Route path={'/'} element={<EducationalYearsIndex/>}/>
            <Route path={':id'} element={<EducationalYearsView/>}/>
            <Route path={'create'} element={<EducationalYearsCreate/>}/>
            <Route path={'update/:id'} element={<EducationalYearsUpdate/>}/>
            <Route path={'*'} element={<Error/>}/>
        </Routes>
    );
};
